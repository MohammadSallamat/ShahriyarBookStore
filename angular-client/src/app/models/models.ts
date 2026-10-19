export interface CategoryMock {
  id: number;
  title: string;
  count: number;
  icon: string;
  parentId?: number;
}

export interface ProductSpecificationMock {
  key: string;
  value: string;
}

export interface BookMock {
  id: number;
  title: string;
  author: string;
  price: number;
  imageUrl: string;
  description: string;
  category: string;
  categoryId?: number;
  rating: number;
  specifications?: ProductSpecificationMock[];
}

export interface CommentMock {
  id: number;
  userName: string;
  userAvatar: string;
  text: string;
  date: string;
  rating: number;
}

export interface CartItemMock {
  book: BookMock;
  quantity: number;
}

export interface OrderItemMock {
  bookTitle: string;
  price: number;
  quantity: number;
}

export interface OrderMock {
  id: number;
  orderNumber: string;
  date: string;
  status: string;
  statusClass: string;
  totalPrice: number;
  items: OrderItemMock[];
}

export interface SellerInventoryMock {
  bookTitle: string;
  stockCount: number;
  price: number;
}

export interface SellerMock {
  id: number;
  shopName: string;
  sellerName: string;
  nationalCode: string;
  status: string;
  rating: number;
  inventories: SellerInventoryMock[];
}

export interface SliderMock {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  linkText: string;
}
