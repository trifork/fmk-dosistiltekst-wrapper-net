using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class DateOrDateTimeWrapper
    {
        public DateTime? Date { get; protected set; }
        public DateTime? DateTime { get; protected set; }
	
	    public static DateOrDateTimeWrapper MakeDate(DateTime date) {
		    return new DateOrDateTimeWrapper(date, null);
	    }
	
	    public static DateOrDateTimeWrapper MakeDate(string date) {
		    return new DateOrDateTimeWrapper(System.DateTime.Parse(date), null);
	    }

	    public static DateOrDateTimeWrapper MakeDateTime(System.DateTime dateTime) {
		    return new DateOrDateTimeWrapper(null, dateTime);
	    }
	
	    public static DateOrDateTimeWrapper MakeDateTime(string dateTime) {
            return new DateOrDateTimeWrapper(null, System.DateTime.Parse(dateTime));
	    }

	    private DateOrDateTimeWrapper(DateTime? date, DateTime? dateTime) {
		    this.Date = date;
		    this.DateTime = dateTime;
	    }

	    public DateTime GetDateOrDateTime() {
            return Date.HasValue ? Date.Value : DateTime.Value;
	    }

        public override bool Equals(object o)
        {
		    if(o==null || !(o is DateOrDateTimeWrapper))
			    return false;
		    
            var dt = (DateOrDateTimeWrapper)o;
		    if(Date.HasValue) 
			    return Date.Value.CompareTo(dt.Date.Value) == 0;
		    else if(DateTime.HasValue) 
			    return DateTime.Value.CompareTo(dt.DateTime) == 0;
		    else 
			    return dt.Date.HasValue == false && dt.DateTime.HasValue == false;
	    }
    }
}
