import classNames from "classnames";
import React, { ReactNode } from "react";
import { PropertyValidationResult } from "../../../services/validation/PropertyValidationResult";
import InputWrapper from "./InputWrapper";

type Props = {
  name?: string;
  value: string | number | string[];
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  validationResult?: PropertyValidationResult | undefined;
  className?: string;
  placeholder?: string;
  type?: "text" | "password" | "number";
  children?: ReactNode;
  onClick?: (event: React.MouseEvent<HTMLInputElement, MouseEvent>) => void;
  readOnly?: boolean;
};

export default function TextInput(props: Props) {
  return (
    <InputWrapper name={props.name} validationResult={props.validationResult}>
      <input
        type={props.type}
        className={classNames(
          "border text-sm bg-gray-100 rounded-md py-1 px-3 text-gray-700 w-full",
          { "border-red-600": props.validationResult?.HasErrors() }
        )}
        value={props.value}
        onChange={(e) => {
          if (props.onChange) {
            props.onChange(e);
          }
          props.validationResult?.Clear();
        }}
        placeholder={props.placeholder}
        readOnly={props.readOnly}
        onClick={props.onClick}
      />
      {props.children && <div className="text-sm ml-2 flex items-center">{props.children}</div>}
    </InputWrapper>
  );
}
