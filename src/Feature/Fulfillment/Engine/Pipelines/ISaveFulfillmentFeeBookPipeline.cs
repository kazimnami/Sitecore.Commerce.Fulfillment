using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using Feature.Fulfillment.Engine;

namespace Feature.Fulfillment.Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IPipeline{SaveFulfillmentFeeBookArgument, FulfillmentFeeBook, CommercePipelineExecutionContext}" />
    /// <seealso cref="IPipelineBlock{SaveFulfillmentFeeBookArgument, FulfillmentFeeBook, CommercePipelineExecutionContext}" />
    /// <seealso cref="IPipelineBlock" />
    /// <seealso cref="IPipeline" />
    [PipelineDisplayName(FulfillmentConstants.Pipelines.SaveFulfillmentFeeBookPipeline)]
    public interface ISaveFulfillmentFeeBookPipeline : IPipeline<SaveFulfillmentFeeBookArgument, FulfillmentFeeBook, CommercePipelineExecutionContext>, IPipelineBlock<SaveFulfillmentFeeBookArgument, FulfillmentFeeBook, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}
