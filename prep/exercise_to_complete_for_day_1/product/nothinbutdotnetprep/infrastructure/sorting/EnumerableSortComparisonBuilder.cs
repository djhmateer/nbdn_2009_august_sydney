using System;
using System.Collections;
using System.Collections.Generic;

namespace nothinbutdotnetprep.infrastructure.sorting
{
    public class EnumerableSortComparisonBuilder<Item> : IEnumerable<Item>
    {
        IEnumerable<Item> items_to_sort;
        SortComparisonBuilder<Item> comparison_builder;

        public EnumerableSortComparisonBuilder(IEnumerable<Item> items_to_sort, SortComparisonBuilder<Item> comparison_builder)
        {
            this.items_to_sort = items_to_sort;
            this.comparison_builder = comparison_builder;
        }

        public EnumerableSortComparisonBuilder<Item> then_by<PropertyType>(Func<Item, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return then_by(accessor, SortOrders.ascending);
        }
        public EnumerableSortComparisonBuilder<Item> then_by<PropertyType>(Func<Item, PropertyType> accessor,SortOrder sort_order)
            where PropertyType : IComparable<PropertyType>
        {
            comparison_builder.then_by(accessor,sort_order);
            return this;
        }

        public EnumerableSortComparisonBuilder<Item> then_using(IComparer<Item> comparer)
        {
            comparison_builder.then_using(comparer);
            return this;
        }

        public EnumerableSortComparisonBuilder<Item> then_with(IComparer<Item> comparer)
        {
            comparison_builder.then_using(comparer);
            return this;
        }

        public IEnumerator<Item> GetEnumerator()
        {
            var list = new List<Item>(items_to_sort);
            list.Sort(comparison_builder);
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}