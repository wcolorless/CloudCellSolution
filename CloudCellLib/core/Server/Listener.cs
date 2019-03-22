using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using CloudCellLib.FuncRunner;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CloudCellLib.Logs;
using CloudCellLib.Statistics;

namespace CloudCellLib.Server
{
    [Serializable]
    public class Listener : INotifyPropertyChanged
    {
        [field: NonSerialized]public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private readonly string _address = "";
        [NonSerialized]
        private HttpListener _Listener;
        private string _FunctionName;
        private IFuncRunner _Runner;
        private Logger _Logger;
        private bool _IsRun = false;
        private string _Status = "Включить";


        private FunctionStatistics _Statistics;
        private Settings _FunctionSettings;

        public FunctionStatistics Statistics
        {
            get
            {
                return _Statistics;
            }
            set
            {
                _Statistics = value;
                NotifyPropertyChanged("Statistics");
            }
        }
        public Settings FunctionSettings
        {
            get
            {
                return _FunctionSettings;
            }
            set
            {
                _FunctionSettings = value;
                NotifyPropertyChanged("FunctionSettings");
            }
        }



        public string FunctionName
        {
            get
            {
                return _FunctionName;
            }
        }


        public bool IsRun
        {
            get
            {
                return _IsRun;
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if(_Status != value)
                {
                    _Status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }

        private Listener(string address, string functionName, Type ServiceType, Logger logger)
        {
            _address = address;
            _Listener = new HttpListener();
            _Listener.Prefixes.Add(address);
            _FunctionName = functionName;
            _Runner = SimpleFuncRunner.Create(ServiceType);
            _Logger = logger;
            _Statistics = new FunctionStatistics();
            _FunctionSettings = new Settings();
        }

        public static Listener Create(string address, string functionName, Type ServiceType, Logger logger)
        {
            return new Listener(address, functionName, ServiceType, logger);
        }

        public async void StartListen()
        {
            if(_IsRun == false)
            {
                if (_Listener == null)
                {
                    _Listener = new HttpListener();
                    _Listener.Prefixes.Add(_address);
                }
                _Logger.AddItem(LogItem.Create("Started function", LogItemType.Start, FunctionName));
                try
                {
                    _Listener.Start();  
                }
                catch(Exception e)
                {
                    throw new InvalidOperationException("Start Function error", e);
                }
                _IsRun = true;
                Status = "Выключить";
                Stopwatch tickCounter = new Stopwatch();
                while (true)
                {
                    tickCounter.Reset();
                    HttpListenerContext CurrentContext;
                    try
                    {
                        CurrentContext = await _Listener.GetContextAsync();
                    }
                    catch (Exception e)
                    {
                        if (_Listener != null && _Listener.IsListening)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    var Request = CurrentContext.Request;
                    _Logger.AddItem(LogItem.Create("Receive exchange", LogItemType.Exchange, FunctionName));
                    Statistics.RequestCount += 1;
                    if (Request.HttpMethod == "GET")
                    {
                        var requestString = Request.QueryString;
                        var GetRequest = Request.RawUrl;
                        Statistics.DataReceive += GetRequest.Length;
                        Dictionary<string, object> Params = GetHttpRequest.GetParams(GetRequest);
                        var par = Params.Values.ToArray();
                        tickCounter.Start();
                        var result = _Runner.Run(_FunctionName, par);
                        tickCounter.Stop();
                        Statistics.TickCounter += tickCounter.ElapsedTicks;
                        var response = CurrentContext.Response;
                        var responseStream = response.OutputStream;
                        if(FunctionSettings.EnableCors)
                        {
                            response.AddHeader("Access-Control-Allow-Headers", FunctionSettings.GetCorsHeaderString);
                        }
                        string responseString;
                        if (result.GetType() != typeof(string))
                        {
                            responseString = result.ToString();
                        }
                        else
                        {
                            responseString = (string)result;
                        }
                        var returnArray = Encoding.UTF8.GetBytes(responseString);
                        response.ContentLength64 = returnArray.Length;
                        responseStream.Write(returnArray, 0, returnArray.Length);
                    }
                    else
                    {
                        var stream = Request.InputStream;
                        byte[] array = new byte[stream.Length];
                        stream.Read(array, 0, array.Length);
                        string strFromRequest = Encoding.UTF8.GetString(array);
                    }
                }
            }
        }
         
        public void StopListen()
        {
            if(_IsRun == true)
            {
                _Logger.AddItem(LogItem.Create("Stoped function", LogItemType.Stop, FunctionName));
                _IsRun = false;
                Status = "Включить";
                _Listener.Stop();
            }
        }




    }
}
