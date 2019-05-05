﻿using System;
using System.Collections.Generic;

namespace OW.Experts.Domain.Infrastructure.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (items == null) throw new ArgumentNullException(nameof(items));

            if (list is List<T> implList) {
                implList.AddRange(items);
            }
            else {
                foreach (var item in items) {
                    list.Add(item);
                }
            }
        }
    }
}