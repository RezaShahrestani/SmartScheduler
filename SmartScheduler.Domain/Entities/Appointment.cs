using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; internal set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public bool IsConfirmed { get; set; }

        public void SetId(Guid? id = null)
        {
            if (id.HasValue)
                Id = id.Value;
            else
                Id = Guid.NewGuid();
        }
    }
}
