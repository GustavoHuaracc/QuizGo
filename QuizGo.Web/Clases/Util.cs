using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using QuizGo.Autentificacion.Entity;
using QuizGo.Web.Clases;

namespace QuizGo.Web.Clases
{
    public static class Util
    {
        public static object MakeRequest(Type autenticar,FiltroAutentificar filtroAutentificar)
        {
            HttpWebResponse response = null;
           
                var listaAutenticar = new List<BeAutentificacion>();

                string requestUrl = "http://localhost:8034/Autentificacion.svc/QuizGo/Autentificar";

                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                WebRequest WR = WebRequest.Create(requestUrl);
                //string sb = JsonConvert.SerializeObject(autenticar);
                string sb = "{\"autentificar\":{\"Usuario\":\"" + filtroAutentificar.Usuario + "\",\"Password\":\"" + filtroAutentificar.Password + "\"}}";
                request.Timeout = Timeout.Infinite;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                Byte[] bt = Encoding.UTF8.GetBytes(sb);
                Stream st = request.GetRequestStream();
                st.Write(bt, 0, bt.Length);
                st.Close();
                response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error al llamar al servicio");
                }
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string strb = sr.ReadToEnd();
                var respuesta = JsonConvert.DeserializeObject<List<BeAutentificacion>>(strb);
                return respuesta;
            
        }
    }
}