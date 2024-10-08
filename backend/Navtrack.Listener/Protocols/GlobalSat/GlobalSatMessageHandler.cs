using System.Text.RegularExpressions;
using Navtrack.DataAccess.Model.Devices.Messages;
using Navtrack.Listener.Helpers;
using Navtrack.Listener.Helpers.New;
using Navtrack.Listener.Server;
using Navtrack.Shared.Library.DI;

namespace Navtrack.Listener.Protocols.GlobalSat;

[Service(typeof(ICustomMessageHandler<GlobalSatProtocol>))]
public class GlobalSatMessageHandler : BaseMessageHandler<GlobalSatProtocol>
{
    public override DeviceMessageDocument Parse(MessageInput input)
    {
        DeviceMessageDocument deviceMessageDocument = Parse(input, ParseFormat0, ParseFormatAlternative);

        return deviceMessageDocument;
    }

    private static DeviceMessageDocument ParseFormat0(MessageInput input)
    {
        input.NetworkStream.Write(StringUtil.ConvertStringToByteArray("ACK\r"));

        Match locationMatch =
            new Regex("(\\d{15})," + // imei
                      "(.*)," + // skip
                      "(\\d)," + // gps fix
                      "(\\d{2})(\\d{2})(\\d{2})," + // dd mm yy
                      "(\\d{2})(\\d{2})(\\d{2})," + // hh mm ss
                      "(E|W)(\\d+.\\d+)," + // longitude
                      "(N|S)(\\d+.\\d+)," + // latitude
                      "(\\d+)," + // altitude
                      "(.*?)," + // speed
                      "(\\d+)," + // heading
                      "(\\d+)," + // number of satellites
                      "(.*?)") // hdop
                .Match(input.DataMessage.String);

        if (locationMatch.Success)
        {
            input.ConnectionContext.SetDevice(locationMatch.Groups[1].Value);

            DeviceMessageDocument deviceMessageDocument = new()
            {
                // Device = input.ConnectionContext.Device,
                Position = new PositionElement
                {
                    Date = DateTimeUtil.New(
                        locationMatch.Groups[6].Value,
                        locationMatch.Groups[5].Value,
                        locationMatch.Groups[4].Value,
                        locationMatch.Groups[7].Value,
                        locationMatch.Groups[8].Value,
                        locationMatch.Groups[9].Value),
                    Valid = locationMatch.Groups[3].Value != "1",
                    Longitude = GpsUtil.ConvertDmmLongToDecimal(locationMatch.Groups[11].Value,
                        locationMatch.Groups[10].Value),
                    Latitude = GpsUtil.ConvertDmmLatToDecimal(locationMatch.Groups[13].Value,
                        locationMatch.Groups[12].Value),
                    Altitude = locationMatch.Groups[14].Get<float?>(),
                    Speed = SpeedUtil.KnotsToKph(locationMatch.Groups[15].Get<float>()),
                    Heading = locationMatch.Groups[16].Get<float?>(),
                    Satellites = locationMatch.Groups[17].Get<short?>(),
                    HDOP = locationMatch.Groups[18].Get<float?>()
                }
            };

            return deviceMessageDocument;
        }

        return null;
    }

    private static DeviceMessageDocument ParseFormatAlternative(MessageInput input)
    {
        Match locationMatch =
            new Regex("(\\d+)," + // imei
                      "(\\d)," + // skip
                      "(\\d+)," + // gps fix
                      "(\\d{2})(\\d{2})(\\d{2})," + // dd mm yy
                      "(\\d{2})(\\d{2})(\\d{2})," + // hh mm ss
                      "(E|W)(\\d+.\\d+)," + // longitude
                      "(N|S)(\\d+.\\d+)," + // latitude
                      "(\\d+.\\d+)," + // altitude
                      "(\\d+.\\d+)," + // speed
                      "(.*?|)," + // heading
                      "(\\d+)" + // satellites
                      "(,(\\d+.\\d+)|)") // hdop
                .Match(input.DataMessage.String);

        if (locationMatch.Success)
        {
            input.ConnectionContext.SetDevice(locationMatch.Groups[1].Value);

            DeviceMessageDocument deviceMessageDocument = new()
            {
                // Device = input.ConnectionContext.Device,
                Position = new PositionElement
                {
                    Date = DateTimeUtil.New(
                        locationMatch.Groups[6].Value,
                        locationMatch.Groups[5].Value,
                        locationMatch.Groups[4].Value,
                        locationMatch.Groups[7].Value,
                        locationMatch.Groups[8].Value,
                        locationMatch.Groups[9].Value),
                    Valid = locationMatch.Groups[2].Value != "1",
                    Longitude = GpsUtil.ConvertDmmLongToDecimal(locationMatch.Groups[11].Value,
                        locationMatch.Groups[10].Value),
                    Latitude = GpsUtil.ConvertDmmLatToDecimal(locationMatch.Groups[13].Value,
                        locationMatch.Groups[12].Value),
                    Altitude = locationMatch.Groups[14].Get<float?>(),
                    Speed = SpeedUtil.KnotsToKph(locationMatch.Groups[15].Get<float>()),
                    Heading = locationMatch.Groups[16].Get<float?>(),
                    Satellites = locationMatch.Groups[17].Get<short?>(),
                    HDOP = locationMatch.Groups[18].Get<float?>()
                }
            };

            return deviceMessageDocument;
        }

        return null;
    }
}