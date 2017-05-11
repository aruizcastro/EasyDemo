using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Json;
using Java.Lang;
using Java.Net;
using Java.IO;

namespace EasyDemo.Droid.Models
{
    public class DataConnection
    {
        static string mFunction, mName, data, mAge, loadData, dataUser;
        static Activity activity;
        static List<Users> useronList;
        static JSONObject json_data; 

        public DataConnection(Activity context,string funcion,string name,string age)
        {
            activity = context;
            mFunction = funcion;
            mName = name;
            mAge = age;
            useronList = new List<Users>();
            new GetAndSet().Execute();
        }

        private static string getData()
        {
            StringBuffer response = null;
            URL obj = new URL("http://192.168.1.17/easyserver/WebService.php");
            switch (mFunction)
            {
                case "setUser":
                    data = "function=" + URLEncoder.Encode(mFunction, "UTF-8")
                        + "&name=" + URLEncoder.Encode(mName, "UTF-8")
                        + "&age=" + URLEncoder.Encode(mAge, "UTF-8");
                    break;
                case "getUser":
                    data = "function=" + URLEncoder.Encode(mFunction, "UTF-8");
                    break;
            }
            //creación del objeto de conexión
            HttpURLConnection con = (HttpURLConnection)obj.OpenConnection();
            //Agregando la cabecera
            //Enviamos la petición por post
            con.RequestMethod = "POST";
            con.DoOutput = true;
            con.DoInput = true;
            con.SetRequestProperty("Content-Type","application/x-www-form-urlencoded");
            //Envio de datos
            //obtener el tamaño de los datos
            con.SetFixedLengthStreamingMode(data.Length);
            /*
             * OutputStream clase abstracta que es la superclase de todas las clases que
             * representan la salida de un flujo de bytes. Un flujo de salida acepta bytes de salida.
             * BufferOutputStream es la clase que implementa un flujo de salida con buffer 
             */

            OutputStream ouT = new BufferedOutputStream(con.OutputStream);
            byte[] array = Encoding.ASCII.GetBytes(data);
            ouT.Write(array);
            ouT.Flush();
            ouT.Close();

            /*
             * Obteniendo datos
             * BufferedRead Lee texto de una corriente de caracteres de entrada,
             * Un InputStreamReader es un puente de flujos de streams de caracteres: se lee los
             * bytes y los decodifica en caracteres             
             */
            BufferedReader iN = new BufferedReader(new InputStreamReader(con.InputStream));
            string inputLine;
            response = new StringBuffer();
            while ((inputLine = iN.ReadLine()) != null )
            {
                response.Append(inputLine);

            }
            iN.Close();
            return response.ToString();

        }

        static bool filterData()
        {
            loadData = getData();
            try {
                if(loadData != "" || loadData != null)
                {
                    json_data = new JSONObject(loadData);
                    switch (mFunction)
                    {
                        case "setUser":
                            dataUser = json_data.GetString("Insert");

                            break;
                        case "getUser":
                            useronList = new List<Users>();
                            JSONArray resultJSON = json_data.GetJSONArray("results");
                            int count = resultJSON.Length();
                            for (int i=0;i < count;i++) {
                                JSONObject jsonNode = resultJSON.GetJSONObject(i);
                                int Id = Convert.ToInt16(jsonNode.OptString("IdUser").ToString());
                                string Name = jsonNode.OptString("Name").ToString();
                                int Age = Convert.ToInt16(jsonNode.OptString("Age").ToString()); ;
                                useronList.Add(new Users()
                                {
                                    Id = Id, Name = Name, Age = Age
                                });
                            }
                            break;
                    }
                }
            }
            catch(System.Exception){

                throw;
            }
            return true;
        }
        private static void Activity()
        {
            Toast.MakeText(activity, dataUser, ToastLength.Long).Show();
        }
        class GetAndSet : AsyncTask<string, string, string>
        {
            protected override string RunInBackground(params string[] @params)
            {
                if (filterData()) {
                    activity.RunOnUiThread(() => {
                        Activity();
                    });
                }
                return null;
            }
        }

        public List<Users> getUsers()
        {
            return useronList;
        }

    }
}