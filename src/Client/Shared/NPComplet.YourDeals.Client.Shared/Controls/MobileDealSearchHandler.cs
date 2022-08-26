using NPComplet.YourDeals.Domain.Shared.Search;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace NPComplet.YourDeals.Client.Shared.Controls
{
    public class MobileDealSearchHandler : SearchHandler
    {
        public IList<Tag> Tags { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                try
                {
                    ItemsSource = Tags
                        .Where(tag => tag.TagString.ToLower().Contains(newValue.ToLower()))
                        .ToList();
                }
                catch
                {
                    ItemsSource = null;
                }
            }
        }

        /// <inheritdoc />
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            // The following route works because route names are unique in this application.
            MessagingCenter.Instance.Send((Tag)item, "SelectedItemSearch");
        }
    }
}
