using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;


namespace ConsoleApplication1
{
    class Program
    {
        public static object requests { get; private set; }

        static void Main()
        {
            //get image into base64string
            string imageFilePath = @"F:\doc.PNG";
            string image_base64string = GetFromImageFile(imageFilePath);


            //input json content
            string jsonstring_orig = @"{ 
                      {
                     ""requests"": [
                                       {
                                 ""image"": {
                                   ""content"": ""rep_image_base64string""},
        
             ""features"": [
                        {
                          ""type"": ""DOCUMENT_TEXT_DETECTION""
                        }
                       ]
                      }
                    ]
                   }";
            string jsonstring = jsonstring_orig.Replace("rep_image_base64string", image_base64string);

       // File.WriteAllText(@"F:\\json.json", jsonstring);

            GoogleTextDection(jsonstring);






        }

        static async void googletextdection(string filePath)
        {
            // Load an image from a local file.
            var image = Image.FromFile(filePath);
            var client = ImageAnnotatorClient.Create();
            var response = client.DetectDocumentText(image);
foreach (var page in response.Pages)
{
                foreach (var block in page.Blocks)
                {
                    foreach (var paragraph in block.Paragraphs)
                    {
                        Console.WriteLine(string.Join("\n", paragraph.Words));
                    }
                }
            }
        }

        //post api
        static async void GoogleTextDection(string jsonstring)
        {

            HttpClient client = new HttpClient();
            //put data
          //  RootObject_post newroot = new RootObject_post();
          //  newroot.requests = new List<Request> {
            
       // };
           
      

               









            var data = JsonConvert.SerializeObject(jsonstring);         
            var content = new StringContent(jsonstring.ToString(), Encoding.UTF8, "application/json");
      
            //need to think about the api_key 
            var response = await client.PostAsync("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyBf3aybUgE0aEvKgFRnBhZVN09V3S-A2js", content);
            // var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result); 
            // return result; 

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            // Display the JSON response.
            Console.WriteLine("\nResponse:\n");
            Console.WriteLine(JsonPrettyPrint(contentString));
            File.WriteAllText(@"F:\\google_text_dection.json", contentString);

        }

        ///get stream from image file
        static string GetFromImageFile(string imageFilePath)
        {

            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);

            byte[] byteData = binaryReader.ReadBytes((int)fileStream.Length);
            string base64String = Convert.ToBase64String(byteData);
            return base64String;




        }



        public class Image
        {
            public string content { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
        }

        public class Request
        {
            public Image image { get; set; }
            public List<Feature> features { get; set; }
    
        }

        public class RootObject_post
        {      

            public List<Request> requests { get; set; }
        }




        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }

        public class Vertex
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingPoly
        {
            public List<Vertex> vertices { get; set; }
        }

        public class TextAnnotation
        {
            public string locale { get; set; }
            public string description { get; set; }
            public BoundingPoly boundingPoly { get; set; }
        }

        public class DetectedLanguage
        {
            public string languageCode { get; set; }
        }

        public class Property
        {
            public List<DetectedLanguage> detectedLanguages { get; set; }
        }

        public class DetectedLanguage2
        {
            public string languageCode { get; set; }
        }

        public class Property2
        {
            public List<DetectedLanguage2> detectedLanguages { get; set; }
        }

        public class Vertex2
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingBox
        {
            public List<Vertex2> vertices { get; set; }
        }

        public class DetectedLanguage3
        {
            public string languageCode { get; set; }
        }

        public class Property3
        {
            public List<DetectedLanguage3> detectedLanguages { get; set; }
        }

        public class Vertex3
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingBox2
        {
            public List<Vertex3> vertices { get; set; }
        }

        public class DetectedLanguage4
        {
            public string languageCode { get; set; }
        }

        public class Property4
        {
            public List<DetectedLanguage4> detectedLanguages { get; set; }
        }

        public class Vertex4
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingBox3
        {
            public List<Vertex4> vertices { get; set; }
        }

        public class DetectedLanguage5
        {
            public string languageCode { get; set; }
        }

        public class DetectedBreak
        {
            public string type { get; set; }
        }

        public class Property5
        {
            public List<DetectedLanguage5> detectedLanguages { get; set; }
            public DetectedBreak detectedBreak { get; set; }
        }

        public class Vertex5
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingBox4
        {
            public List<Vertex5> vertices { get; set; }
        }

        public class Symbol
        {
            public Property5 property { get; set; }
            public BoundingBox4 boundingBox { get; set; }
            public string text { get; set; }
        }

        public class Word
        {
            public Property4 property { get; set; }
            public BoundingBox3 boundingBox { get; set; }
            public List<Symbol> symbols { get; set; }
        }

        public class Paragraph
        {
            public Property3 property { get; set; }
            public BoundingBox2 boundingBox { get; set; }
            public List<Word> words { get; set; }
        }

        public class Block
        {
            public Property2 property { get; set; }
            public BoundingBox boundingBox { get; set; }
            public List<Paragraph> paragraphs { get; set; }
            public string blockType { get; set; }
        }

        public class Page
        {
            public Property property { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public List<Block> blocks { get; set; }
        }

        public class FullTextAnnotation
        {
            public List<Page> pages { get; set; }
            public string text { get; set; }
        }

        public class Respons
        {
            public List<TextAnnotation> textAnnotations { get; set; }
            public FullTextAnnotation fullTextAnnotation { get; set; }
        }

        public class RootObject
        {
            public List<Respons> responses { get; set; }
        }



    }
}
