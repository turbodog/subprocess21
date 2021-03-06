﻿using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Twistlock;

namespace SubprocessLambdaNamespace
{
  public class MyHandlerClass {
   [LambdaSerializer(typeof(JsonSerializer))]

   public Result HandleFunction(Request request)
   {
     // From the AWS console, test the function with JSON in the form of
     // {
     //  "target": "http://example.com"
     // }
     // This function will do an HTTP GET and return the first 256 bytes and the return code

      string target;
      target = request.Target;
      using (HttpClient client = new HttpClient())
      {
          Console.WriteLine($"Sending HTTP request.");
          HttpResponseMessage response = client.GetAsync(target).Result;
          Console.WriteLine($"Response received.");
          var content = response.Content.ReadAsStringAsync().Result;
          Console.WriteLine($"Response content: {content}");
          return new Result {output=content.Substring(0,256), returncode=response.StatusCode, isprotected="Protected by Prisma Cloud Compute"};
      }
   }
   [LambdaSerializer(typeof(JsonSerializer))]
   public Result ProtectedHandleFunction(Request request)
   {
     return Twistlock.Serverless.Handler<Request, Result>(HandleFunction, request);
   }
 }
 public class Request{
  public string Target { get; set; }
 }
 public class Result {
  public string output { get; set; }
  public string isprotected { get; set; }
  public System.Net.HttpStatusCode returncode { get; set; }
 }
}