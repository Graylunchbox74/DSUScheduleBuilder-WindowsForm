using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Main_Menu
{
    /// <summary>
    /// Represents something that can be reset to a default state
    /// </summary>
    public interface IResetable
    {
        void ResetToDefault();
    }
}
