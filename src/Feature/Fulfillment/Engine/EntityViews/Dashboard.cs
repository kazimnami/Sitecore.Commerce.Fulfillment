
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.BusinessUsers;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Feature.Fulfillment.Engine
{
    [PipelineDisplayName(FulfillmentConstants.Pipelines.Blocks.GetFulfillmentDashboardViewBlock)]
    public class Dashboard : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dashboard"/> class.
        /// </summary>
        /// <param name="commerceCommander">The <see cref="CommerceCommander"/> is a gateway object to resolving and executing other Commerce Commands and other control points.</param>
        public Dashboard(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        /// <summary>The execute.</summary>
        /// <param name="entityView">The argument.</param>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="EntityView"/>.</returns>
        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            if (entityView.Name != FulfillmentConstants.EntityViews.Dashboard)
            {
                return entityView;
            }

            //TODO find out about changing icon
            entityView.UiHint = "Flat";
            entityView.Icon = "market_stand";
            entityView.DisplayName = "Marketplaces";

            var ebayMarketplaceView = new EntityView
            {
                Name = "EbayMarketplace",
                DisplayName = "Ebay Marketplace",
                UiHint = "Flat",
                Icon = "market_stand",
                ItemId = "",
            };
            entityView.ChildViews.Add(ebayMarketplaceView);

            

            ebayMarketplaceView.Properties.Add(
                new ViewProperty
                {
                    Name = "",
                    IsHidden = false,
                    IsReadOnly = true,
                    OriginalType = "Html",
                    UiType = "Html",
                    RawValue = "<img alt='This is the alternate' height=50 width=100 src='https://www.paypalobjects.com/webstatic/en_AR/mktg/merchant/pages/sell-on-ebay/image-ebay.png' style=''/>"
                });

            var ebayPendingSellableItems = await this._commerceCommander.Command<ListCommander>()
                            .GetListItems<SellableItem>(context.CommerceContext, "Ebay_Pending", 0, 10);


            //var ebayPendingSellableItemsView = new EntityView
            //{
            //    EntityId = "",
            //    ItemId = "Id",
            //    DisplayName = "Pending Items",
            //    Name = "PendingItems",
            //    UiHint = "Table"
            //};
            //entityView.ChildViews.Add(ebayPendingSellableItemsView);

            //foreach (var sellableItem in ebayPendingSellableItems)
            //{
            //    var pathsView1 = new EntityView
            //    {
            //        EntityId = "",
            //        ItemId = sellableItem.Id,
            //        DisplayName = "Sellable Item",
            //        Name = "SellableItem",
            //        UiHint = ""
            //    };
            //    ebayPendingSellableItemsView.ChildViews.Add(pathsView1);

            //    pathsView1.Properties
            //    .Add(new ViewProperty { Name = "Name", RawValue = sellableItem.Name, UiType = "EntityLink" });

            //    if (sellableItem.HasComponent<EbayItemComponent>())
            //    {
            //        var ebayItemComponent = sellableItem.GetComponent<EbayItemComponent>();

            //        //pathsView1.Properties
            //        //    .Add(new ViewProperty { Name = "ItemId", RawValue = sellableItem.Id });
            //        pathsView1.Properties
            //            .Add(new ViewProperty { Name = "Status", RawValue = ebayItemComponent.Status });
            //        pathsView1.Properties
            //            .Add(new ViewProperty { Name = "EbayId", RawValue = ebayItemComponent.EbayId });
            //    }
            //}

            return entityView;
        }
    }
}
