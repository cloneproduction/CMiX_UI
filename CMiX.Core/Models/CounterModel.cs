// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class CounterModel : Model
    {
        public CounterModel()
        {
            this.ID = Guid.NewGuid();
        }

        public int Count { get; set; }
    }
}