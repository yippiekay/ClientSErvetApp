using AutoMapper;
using ClientApp.ApiService;
using ClientApp.Models;
using System.IO;
using System.Windows.Media.Imaging;

namespace ClientApp.AutoMapper
{
    public class Configuration 
    {
        public static MapperConfiguration GetConfiguration() {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Card, CardModel>()
                .ForMember(dest => dest.ImageSource, opt => opt.MapFrom(src => ByteToImage(src.ImageByteCode)));
                
                cfg.CreateMap<CardModel, Card>()
                .ForMember(dest => dest.ImageByteCode, opt => opt.MapFrom(src => ImageToByte(src.ImageSource)));
            });
        }

        private static BitmapImage ByteToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                
                return image;
            }
        }

        private static byte[] ImageToByte(BitmapImage bitmapSource)
        {
            var encoder = new JpegBitmapEncoder { QualityLevel = 100 };
            byte[] bit = new byte[0];

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                bit = stream.ToArray();
                stream.Close();
            }

            return bit;
        }
    }
}
