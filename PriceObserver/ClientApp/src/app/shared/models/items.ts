import { Item } from "./item";
import { Shop } from "./shop";

export class Items {

  public shop: Shop = new Shop();

  public items: Item[] | [];
}
