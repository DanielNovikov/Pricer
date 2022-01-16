import { Item } from "./item";

export class Shop {

  public address: string | undefined;
  public logoFileName: string | undefined;
  public currencySign: string | undefined;

  public items: Item[] | [];
}
