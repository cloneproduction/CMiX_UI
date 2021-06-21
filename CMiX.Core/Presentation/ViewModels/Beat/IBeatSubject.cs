﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace CMiX.Core.Presentation.ViewModels.Beat
{
    public interface IBeatSubject
    {
        void Attach(IBeatObserver observer);

        void Detach(IBeatObserver observer);

        void NotifyBeatChange(double period);
    }
}