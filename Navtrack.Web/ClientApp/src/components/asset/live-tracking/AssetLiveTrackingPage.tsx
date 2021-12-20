import { FormattedMessage } from "react-intl";
import useCurrentAsset from "../../../hooks/assets/useCurrentAsset";
import Card from "../../ui/shared/card/Card";
import Map from "../../ui/shared/map/Map";
import LocationBar from "../shared/location-bar/LocationBar";
import LiveTrackingMapPin from "./LiveTrackingMapPin";

export default function AssetLiveTrackingPage() {
  const { currentAsset } = useCurrentAsset();

  return (
    <>
      {currentAsset?.location ? (
        <>
          <Card className="p-2">
            <LocationBar location={currentAsset.location} />
          </Card>
          <div className="rounded-lg shadow flex flex-grow bg-white">
            <Map
              center={{
                latitude: 0,
                longitude: 0
              }}
              zoom={16}>
              <LiveTrackingMapPin />
            </Map>
          </div>
        </>
      ) : (
        <Card className="flex p-2 text-sm">
          <FormattedMessage id="no-location-data" />
        </Card>
      )}
    </>
  );
}