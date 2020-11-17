// namespace Demo.SalesAnalyzerDurableFunction.Utility
// {
//     using System;
//     using System.IO;
//     using System.Linq;
//     using System.Net.Http.Headers;
//
//     public static class MultipartRequestHelper
//     {
//         public static string GetBoundary(MediaTypeHeaderValue contentType)
//         {
//             if (!IsMultipartContentType(contentType.MediaType))
//             {
//                 throw new InvalidDataException(
//                     $"Incorrect media type. Expected multipart, received '{contentType.MediaType}'");
//             }
//
//             if (contentType.Parameters.Count < 1)
//             {
//                 throw new InvalidDataException($"Multipart boundary not found in header.");
//             }
//
//
//             return contentType.Parameters.First().Value;
//         }
//
//         public static bool IsMultipartContentType(string contentType)
//         {
//             return !string.IsNullOrEmpty(contentType)
//                    && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
//         }
//
//         public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
//         {
//             // Content-Disposition: form-data; name="key";
//             return contentDisposition != null
//                    && contentDisposition.DispositionType.Equals("form-data")
//                    && string.IsNullOrEmpty(contentDisposition.FileName.Value)
//                    && string.IsNullOrEmpty(contentDisposition.FileNameStar.Value);
//         }
//
//         public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
//         {
//             // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
//             return contentDisposition != null
//                    && contentDisposition.DispositionType.Equals("form-data")
//                    && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
//                        || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
//         }
//     }
// }