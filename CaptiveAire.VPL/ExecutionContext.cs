﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CaptiveAire.VPL.Interfaces;
using CaptiveAire.VPL.Metadata;

namespace CaptiveAire.VPL
{
    /// <summary>
    /// This class will be instantiated for the lifetime of a function execution. The initial function may
    /// call other functions.
    /// </summary>
    internal class ExecutionContext : IExecutionContext
    {
        private readonly IVplServiceContext _serviceContext;
        private readonly IDictionary<Guid, FunctionMetadata> _cachedFunctions = new ConcurrentDictionary<Guid, FunctionMetadata>();
        private readonly Lazy<IFunctionService> _functionService;

        private readonly CallStack _callStack = new CallStack();

        internal ExecutionContext(IVplServiceContext serviceContext)
        {
            if (serviceContext == null) throw new ArgumentNullException(nameof(serviceContext));
            _serviceContext = serviceContext;

            _functionService =
                new Lazy<IFunctionService>(() => serviceContext.Services.OfType<IFunctionService>().FirstOrDefault());
        }
        
        private FunctionMetadata GetFunctionMetadata(Guid functionId)
        {
            FunctionMetadata functionMetadata;

            //Check to see if it's in the cache yet
            if (_cachedFunctions.TryGetValue(functionId, out functionMetadata))
            {
                return functionMetadata;
            }

            //Check to see if we have a function service.
            if (_functionService.Value == null)
            {
                //Well snap - we can't do this.
                throw new InvalidOperationException($"No {nameof(IFunctionService)} implementation was provided.");
            }

            //Grab the function
            functionMetadata = _functionService.Value.GetFunction(functionId);

            //Cache the function
            _cachedFunctions[functionId] = functionMetadata;
            
            //Return the function
            return functionMetadata;
        }

        public async Task<object> ExecuteAsync(IFunction function, object[] parameters, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Pushing function '{function.Name}' onto the stack.");

            _callStack.Push(new CallStackFrame(function, _callStack.Count));
           
            //Execute the function
            object result = await function.ExecuteAsync(parameters, this, cancellationToken);

            Debug.WriteLine($"Popping function '{function.Name}' from the stack with result '{result}'.");

            _callStack.Pop();

            return result;
        }

        public async Task<object> ExecuteFunctionAsync(Guid functionId, object[] parameters, CancellationToken cancellationToken)
        {
            //Get the function metadata
            var functionMetadata = GetFunctionMetadata(functionId);

            //Create a new instance of the function
            var function = _serviceContext.ElementBuilder.LoadFunction(functionMetadata);

            //Execute the function
            return await ExecuteAsync(function, parameters,  cancellationToken);
        }

        public ICallStack CallStack
        {
            get { return _callStack; }
        }

        public void Dispose()
        {
            Debug.WriteLine("ExecutionContext disposed.");
        }
    }
}