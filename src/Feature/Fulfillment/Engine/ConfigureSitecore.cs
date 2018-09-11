using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Commerce.EntityViews;

namespace Feature.Fulfillment.Engine
{
    /// <summary>
    /// The carts configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config

                .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure
                    .Add<ConfigureServiceApiBlock>())

                // some configuration is done in CartPipelineExtentions in the engine project.
                .ConfigurePipeline<ICalculateCartPipeline>(configure => configure
                      .Add<CalculateCartFulfillmentBlock>().After<CalculateCartSubTotalsBlock>(), order: 1001)

               .ConfigurePipeline<IGetSellableItemPipeline>(configure => configure
                    .Add<SellableItemFulfillmentBlock>().After<GetSellableItemBlock>())

                .ConfigurePipeline<IAddCartLinePipeline>(configure => configure
                    .Add<AddCartLineFulfillmentBlock>().After<AddCartLineBlock>())

                .AddPipeline<ISaveFulfillmentFeeBookPipeline, SaveFulfillmentFeeBookPipeline>(configure => configure
                    .Add<SaveFulfillmentFeeBook>()
                    .Add<PersistFulfillmentFeeBookBlock>())

                .ConfigurePipeline<IGetEntityViewPipeline>(configure => configure
                    .Add<Dashboard>().Before<IFormatEntityViewPipeline>())
            );

            services.RegisterAllCommands(assembly);
        }
    }
}