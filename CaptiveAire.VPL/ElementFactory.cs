﻿using System;
using CaptiveAire.VPL.Interfaces;

namespace CaptiveAire.VPL
{
    public class ElementFactory : IElementFactory
    {
        private readonly Guid _elementTypeId;
        private readonly string _category;
        private readonly string _name;
        private readonly Func<IElementCreationContext, IElement> _factory;
        private readonly Type _elementType;
        private readonly Guid? _returnType;
        private readonly bool _showInToolbox;
        private readonly string _description;

        public ElementFactory(
            Guid elementTypeId, 
            string category, 
            string name, 
            Func<IElementCreationContext, IElement> factory, 
            Type elementType, 
            Guid? returnType = null, 
            bool showInToolbox = true, 
            string description = null)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            _elementTypeId = elementTypeId;
            _category = category;
            _name = name;
            _factory = factory;
            _elementType = elementType;
            _returnType = returnType;
            _showInToolbox = showInToolbox;
            _description = description;
        }

        public Guid ElementTypeId
        {
            get { return _elementTypeId; }
        }

        public string Category
        {
            get { return _category; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Type ElementType
        {
            get { return _elementType; }
        }

        public Guid? ReturnType
        {
            get { return _returnType; }
        }

        public bool ShowInToolbox
        {
            get { return _showInToolbox; }
        }

        public string Description
        {
            get { return _description; }
        }

        public IElement Create(IElementCreationContext context)
        {
            return _factory(context);
        }
    }
}