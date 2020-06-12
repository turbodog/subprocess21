Prisma Cloud Compute .NET Core Example Lambda Function
================================================

An example .NET Core 2.1 function that can be protected with the Prisma Cloud Compute embedded Defender and show serverless audit events in the Console. The function takes JSON input and does an HTTP GET on the value of the "target" key. If you have a serverless runtime defense rule to alert or block on non-permitted DNS queries, then the alert will fire.

Usage:
- Build and upload the function to AWS
- Create serverless defense runtime rule 
- Create a test event in the form of: 
`{
  "target": "http://github.com"
}`
- Run the function from the AWS console
- Observe Serverless Audits event in the Console
