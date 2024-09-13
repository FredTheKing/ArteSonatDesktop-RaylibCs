using System.IO.Compression;
using IronZip;
using IronZip.Enum;

string key = "top secret key!!!1";
bool encrypt = true;


if (encrypt)
{
  using (var archive = new IronZipArchive(9))
  {
    archive.Encrypt(key, EncryptionMethods.Traditional);
  
    archive.Add("Resources/photo.jpg");
  
    archive.SaveAs("Resources.zip");
  }
}
else
{
  IronZipArchive.ExtractArchiveToDirectory("Resources.zip", "ResourcesOutput");
}