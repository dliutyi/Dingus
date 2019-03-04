using System.Collections;
using Xamarin.Forms;

namespace Dingus.UI
{
    class BindableStackLayout : FlexLayout
    {
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableStackLayout),
                                    propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateItems());

        public DataTemplate ItemDataTemplate
        {
            get => (DataTemplate)GetValue(ItemDataTemplateProperty);
            set => SetValue(ItemDataTemplateProperty, value);
        }

        public static readonly BindableProperty ItemDataTemplateProperty =
            BindableProperty.Create(nameof(ItemDataTemplate), typeof(DataTemplate), typeof(BindableStackLayout));

        void PopulateItems()
        {
            if (ItemsSource == null)
            {
                return;
            }
            foreach (var item in ItemsSource)
            {
                View itemTemplate = ItemDataTemplate.CreateContent() as View;
                itemTemplate.BindingContext = item;
                Children.Add(itemTemplate);
            }
        }
    }
}
