﻿using System.Threading;
using System.Threading.Tasks;
using CaptiveAire.VPL.Extensions;
using CaptiveAire.VPL.Interfaces;

namespace CaptiveAire.VPL.Plugins.Control
{
    public class RepeatStatement : CompoundStatement
    {
        private readonly IParameter Condition;
        private readonly IBlock Block;

        public RepeatStatement(IElementCreationContext context) 
            : base(context)
        {
            Condition = Owner.CreateParameter("condition", Owner.GetFloatType());

            Block = Owner.CreateBlock("block", "Repeat");

            Block.Parameters.Add(Condition);
            Blocks.Add(Block);
        }

        protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            double repeatCount = (double) await Condition.EvaluateAsync(cancellationToken);

            for (double index = 0; index < repeatCount; index++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Block.ExecuteAsync(cancellationToken);
            }
        }
    }
}