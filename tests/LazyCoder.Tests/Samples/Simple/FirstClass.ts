import lib from "npmpackagename";
import { SecondClass } from "./Simple/SecondClass";

export interface FirstClass {
    StringProperty: string;
    StringNullableProperty: (string | null);
    SecondClassProperty: SecondClass;
}
