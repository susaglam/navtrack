import { useGetAssetsAssetIdTrips } from "../../api";

export type IUseTripsQuery = {
  assetId?: string;
  startDate?: string;
  endDate?: string;
  minAltitude?: number;
  maxAltitude?: number;
  minDuration?: number;
  maxDuration?: number;
  minSpeed?: number;
  maxSpeed?: number;
  latitude?: number;
  longitude?: number;
  radius?: number;
};

export default function useTripsQuery(props: IUseTripsQuery) {
  const query = useGetAssetsAssetIdTrips(
    props.assetId as string,
    {
      StartDate: props.startDate,
      EndDate: props.endDate,
      MinAvgSpeed: props.minSpeed,
      MaxAvgSpeed: props.maxSpeed,
      MinAvgAltitude: props.minAltitude,
      MaxAvgAltitude: props.maxAltitude,
      MinDuration: props.minDuration,
      MaxDuration: props.maxDuration,
      Latitude: props.latitude,
      Longitude: props.longitude,
      Radius: props.radius
    },
    {
      query: {
        enabled: !!props.assetId,
        refetchOnWindowFocus: false
      }
    }
  );

  return query;
}