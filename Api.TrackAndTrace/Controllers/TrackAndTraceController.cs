using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;
using Api.TrackAndTrace.Models.DataModel;
using Api.TrackAndTrace.Models.DataRepository;

namespace Api.TrackAndTrace.Controllers
{
    public class TrackAndTraceController : ApiController
    {

        //Phần API trả về dữ liệu của itemcode theo kiểu convert dataset >> JSON
        [AcceptVerbs("GET")]
        [Route("TrackAndTrace")]
        public string GetTraceItem(string itemcode)
        {
            TrackAndTrace_VNPOST.TrackAndTrace Tracking = new TrackAndTrace_VNPOST.TrackAndTrace();
            TrackAndTrace_VNPOST.UserCredentical uc = new TrackAndTrace_VNPOST.UserCredentical();
            uc.user = "vnpost";
            uc.pass = "vn!@#post";
            Tracking.EnableDecompression = true;
            Tracking.UserCredenticalValue = uc;
            Tracking.Timeout = 60000;
            DataSet ds = new DataSet();
            ds = Tracking.TrackAndTrace_Items(itemcode);
            string json = JsonConvert.SerializeObject(ds);
            return json;

        }


        //Phần API trả về dữ liệu của emscode theo kiểu convert dataset >> LIST >> JSON dành cho merchant site
        TrackAndTraceRepository trackandtraceRepository = new TrackAndTraceRepository();
        [AcceptVerbs("GET")]
        [Route("TrackAndTraceItemCode")]
        public ReturnTrackAndTrace ListTrackAndTrace(string emscode)
        {
            ReturnTrackAndTrace returntrackandtrace = new ReturnTrackAndTrace();
            returntrackandtrace =  trackandtraceRepository.ListTrackAndTrace(emscode);
            return returntrackandtrace;
        }

        //Phần API trả về dữ liệu của emscode theo kiểu convert dataset >> LIST >> JSON dành cho merchant site
        [AcceptVerbs("GET")]
        [Route("TrackAndTrace/WebReport")]
        public ReturnTrackAndTrace_WebReport ListTrackAndTrace_WebReport(string emscode)
        {
            ReturnTrackAndTrace_WebReport returntrackandtrace = new ReturnTrackAndTrace_WebReport();
            returntrackandtrace = trackandtraceRepository.ListTrackAndTrace_WebReport(emscode);
            return returntrackandtrace;
        }




    }
}
