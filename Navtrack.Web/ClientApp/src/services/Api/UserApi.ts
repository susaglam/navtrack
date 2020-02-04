import { HttpClient } from "../HttpClient/HttpClient";
import { UserModel } from "./Model/UserModel";
import { ResponseModel } from "./Model/ResponseModel";

export const UserApi = {
  get: function (id: number): Promise<UserModel> {
    return HttpClient.get<UserModel>("users/" + id);
  },

  getAll: function (): Promise<UserModel[]> {
    return HttpClient.get<UserModel[]>("users");
  },

  delete: function (id: number): Promise<ResponseModel> {
    return HttpClient.delete("users/" + id);
  },

  update: function (asset: UserModel): Promise<ResponseModel> {
    return HttpClient.put("users/" + asset.id, asset);
  },

  add: function (asset: UserModel): Promise<ResponseModel> {
    return HttpClient.post("users", asset);
  }
}