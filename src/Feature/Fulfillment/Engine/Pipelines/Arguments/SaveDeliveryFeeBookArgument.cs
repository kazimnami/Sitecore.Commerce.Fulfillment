using Sitecore.Framework.Conditions;
using System.Collections.Generic;
using Feature.Fulfillment.Engine;

namespace Feature.Fulfillment.Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Feature.Fulfillment.Engine.FulfillmentFeeBookArgument" />
    public class SaveFulfillmentFeeBookArgument : FulfillmentFeeBookArgument
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Fulfillment fees.
        /// </summary>
        /// <value>
        /// The Fulfillment fees.
        /// </value>
        public IList<FulfillmentFee> FulfillmentFees { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFulfillmentFeeBookArgument"/> class.
        /// </summary>
        /// <param name="FulfillmentFeeBook">The Fulfillment fee book.</param>
        /// <param name="name">The name.</param>
        /// <param name="FulfillmentFees">The Fulfillment fees.</param>
        public SaveFulfillmentFeeBookArgument(FulfillmentFeeBook FulfillmentFeeBook, string name, IList<FulfillmentFee> FulfillmentFees)
            : base(FulfillmentFeeBook)
        {
            Condition.Requires(name).IsNotNull("The Fulfillment fee book name can not be null or empty");
            this.Name = name;

            Condition.Requires(FulfillmentFees).IsNotNull("The Fulfillment fee book fees collection can not be null or empty");
            this.FulfillmentFees = FulfillmentFees;
        }
    }
}