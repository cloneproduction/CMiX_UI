using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiXPlayer.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {

        }

        public List<IJob> RunningJobs { get; set; }
        public List<DeviceModel> DeviceModels { get; set; }
        public List<PlaylistModel> PlaylistModels { get; set; }
    }
}
