import { Fragment } from "react";
import { Menu, Transition } from "@headlessui/react";
import {
  faSignOutAlt,
  faSlidersH,
  faUser
} from "@fortawesome/free-solid-svg-icons";
import Icon from "../../shared/icon/Icon";
import classNames from "classnames";
import { useLogout } from "../../../../hooks/authentication/useLogout";
import useCurrentUser from "../../../../hooks/app/useCurrentUser";
import { FormattedMessage } from "react-intl";
import { useHistory } from "react-router";
import IconWithText from "../../shared/icon/IconWithText";

export interface IAdminLayoutNavBarProfile {}

export default function AdminLayoutNavBarProfile(
  props: IAdminLayoutNavBarProfile
) {
  const currentUser = useCurrentUser();
  const logout = useLogout();
  const history = useHistory();

  return (
    <Menu as="div" className="ml-3 relative flex">
      <Menu.Button className="flex items-center rounded-full text-xl focus:outline-none focus:ring-2 focus:ring-offset-4 focus:ring-indigo-500">
        <Icon icon={faUser} />
      </Menu.Button>
      <Transition
        as={Fragment}
        enter="transition ease-out duration-100"
        enterFrom="transform opacity-0 scale-95"
        enterTo="transform opacity-100 scale-100"
        leave="transition ease-in duration-75"
        leaveFrom="transform opacity-100 scale-100"
        leaveTo="transform opacity-0 scale-95">
        <Menu.Items className="divide-y divide-gray-100 origin-top-right absolute right-0 mt-6 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none">
          <div className="py-1">
            <Menu.Item disabled>
              <div className="bg-white px-4 py-2 text-sm text-gray-700 cursor-default">
                <FormattedMessage id="navbar.profile.logged-in-as" />{" "}
                <span className="font-semibold">{currentUser?.email}</span>
              </div>
            </Menu.Item>
          </div>
          <div className="py-1">
            <Menu.Item>
              {({ active }) => (
                <div
                  onClick={() => history.push("/settings/account")}
                  className={classNames(
                    active ? "bg-gray-100" : "",
                    "block px-4 py-2 text-sm text-gray-700 cursor-pointer"
                  )}>
                  <IconWithText icon={faSlidersH}>
                    <FormattedMessage id="navbar.profile.settings" />
                  </IconWithText>
                </div>
              )}
            </Menu.Item>
            <Menu.Item>
              {({ active }) => (
                <div
                  onClick={logout}
                  className={classNames(
                    active ? "bg-gray-100" : "",
                    "block px-4 py-2 text-sm text-gray-700 cursor-pointer"
                  )}>
                  <IconWithText icon={faSignOutAlt}>
                    <FormattedMessage id="navbar.profile.logout" />
                  </IconWithText>
                </div>
              )}
            </Menu.Item>
          </div>
        </Menu.Items>
      </Transition>
    </Menu>
  );
}