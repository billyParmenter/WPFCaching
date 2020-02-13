/**
 * @file    WrapperViewmodel.cs
 * @author  Drew Hoffer & Trent Thompson
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCaching
{ 
    public class WrapperViewModel : BaseViewModel
    {
        protected NavigationService service { get; private set; }

        public WrapperViewModel() {
            if (service == null) { 
                service = NavigationService.Instance;
            }
        }

        /**
        *  Navigates to the specified navigation object
        *  
        *  @param  WrapperViewModel navigationObject : Navigation object representing 
        *                                              the place to navigate to.
        *          
        *  @return Void
        */
        protected virtual void Navigate(WrapperViewModel navigationObject)
        {
            service.Navigate(navigationObject);
        }

        /**
        *  Navigates to previous window/view
        *  
        *  @param  Void
        *          
        *  @return Void
        */
        protected virtual void NavigateBack()
        {
            service.NavigateToPrevious();
        }
    }
}
