// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Models.Scheduling
{
    public class CompositionSchedulerModel : Model
    {
        public CompositionSchedulerModel()
        {
            this.ID = Guid.NewGuid();

            JobSchedulerModel = new JobSchedulerModel();
            JobEditorModel = new JobEditorModel();
            PlaylistEditorModel = new PlaylistEditorModel();
        }

        public string Name { get; set; }
        public JobSchedulerModel JobSchedulerModel { get; set; }
        public JobEditorModel JobEditorModel { get; set; }
        public PlaylistEditorModel PlaylistEditorModel { get; set; }
    }
}
