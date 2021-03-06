﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

namespace SendGoMessages
{
    class SendRequest
    {
        /// <summary>
        /// Get, Post 전송
        /// </summary>
        /// <param name="SendMethod"></param>
        /// <param name="PostData"></param>
        /// <param name="SearchUrl"></param>
        /// <returns></returns>
        public static String SendWebRequest(string SendMethod, string PostData, string SearchUrl)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(SearchUrl);
                // Set the Method property of the request to POST.
                //request.Method = "POST";  //rain 수정
                request.Method = SendMethod;
                // Create POST data and convert it to a byte array.
                //string postData = string.Format("eventname={0}&pinnumber={1}", eventname, pinnumber);  //rain 수정
                byte[] byteArray = Encoding.UTF8.GetBytes(PostData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();


                return responseFromServer;


            }
            catch (Exception ex)
            {
                return "Error1";
            }
        }
    }
}
