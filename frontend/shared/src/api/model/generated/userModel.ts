/**
 * Generated by orval v6.24.0 🍺
 * Do not edit manually.
 * Navtrack.Api
 * OpenAPI spec version: 1.0.0
 */
import type { UserAssetRoleModel } from "./userAssetRoleModel";
import type { UnitsType } from "./unitsType";

export interface UserModel {
  assetRoles?: UserAssetRoleModel[] | null;
  email: string;
  id: string;
  units: UnitsType;
}
