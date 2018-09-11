using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Pipelines;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Feature.Fulfillment.Engine;

namespace Feature.Fulfillment.Engine
{
    /// <summary>
    /// Sellable Item Fulfillment Block
    /// </summary>
    /// <seealso cref="PipelineBlock{SellableItem, SellableItem, CommercePipelineExecutionContext}" />
    [PipelineDisplayName(FulfillmentConstants.Pipelines.Blocks.SellableItemFulfillmentBlock)]
    public class SellableItemFulfillmentBlock : PipelineBlock<SellableItem, SellableItem, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellableItemFulfillmentBlock"/> class.
        /// </summary>
        public SellableItemFulfillmentBlock()
          : base(null)
        {
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Task<SellableItem> Run(SellableItem arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull("The argument can not be null");

            var sellableItem = context.CommerceContext.GetObjects<SellableItem>().FirstOrDefault(p => p.ProductId.Equals(arg.FriendlyId, StringComparison.OrdinalIgnoreCase));

            //TODO - re-implement the retrievale of the sellable item
            //if (sellableItem[FulfillmentConstants.ProductAttributes.FulfillmentTypeAttributeName] != null)
            //{
            //    arg.GetComponent<FulfillmentFeeComponent>().FulfillmentTypeName = sellableItem[FulfillmentConstants.ProductAttributes.FulfillmentTypeAttributeName].ToString();
            //}

            return Task.FromResult(arg);
        }
    }
}