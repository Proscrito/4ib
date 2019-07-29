namespace Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels
{
    public class JobSelectListItem
    {        //
        // Summary:
        //     Gets or sets a value that indicates whether this System.Web.Mvc.SelectListItem
        //     is disabled.
        public bool Disabled { get; set; }
        //
        // Summary:
        //     Represents the optgroup HTML element this item is wrapped into. In a select list,
        //     multiple groups with the same name are supported. They are compared with reference
        //     equality.
        public JobSelectListGroup Group { get; set; }
        //
        // Summary:
        //     Gets or sets a value that indicates whether this System.Web.Mvc.SelectListItem
        //     is selected.
        //
        // Returns:
        //     true if the item is selected; otherwise, false.
        public bool Selected { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the selected item.
        //
        // Returns:
        //     The text.
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the value of the selected item.
        //
        // Returns:
        //     The value.
        public string Value { get; set; }
    }
}
