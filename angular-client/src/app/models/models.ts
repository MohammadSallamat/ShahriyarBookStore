export interface CategoryMock {
  id: number;
  title: string;
  count: number;
  icon: string;
  parentId?: number;
}

export interface ProductSpecificationMock {
export interface SeoData {
  metaTitle?: string;
  metaDescription?: string;
  metaKeyWords?: string;
  indexPage?: boolean;
  canonical?: string;
  schema?: string;
}

export interface CategoryDto {
  id: number;
  creationDate: string;
  title: string;
  slug: string;
  seoData?: SeoData;
  parentId?: number;
  childs: CategoryDto[];
}

export interface ProductImageDto {
  id: number;
  creationDate: string;
  productId: number;
  imageName: string;
  sequence: number;
}

export interface ProductSpecificationDto {
  id: number;
  creationDate: string;
  productId: number;
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
export interface ProductDto {
  id: number;
  creationDate: string;
  title: string;
  imageName: string;
  description: string;
  categoryId: number;
  subCategoryId: number;
  secondarySubCategoryId: number;
  slug: string;
  seoData?: SeoData;
  images: ProductImageDto[];
  specifications: ProductSpecificationDto[];
}

export interface ProductFilterParams {
  search?: string;
  categoryId?: number;
  pageId: number;
  take: number;
}

export interface ProductFilterResult {
  products: ProductDto[];
  totalCount: number;
  pageId: number;
  take: number;
  pageCount: number;
}

export interface CommentDto {
  id: number;
  creationDate: string;
  userId: number;
  productId: number;
  text: string;
  status: number;
  updateDate: string;
}

export interface CommentFilterResult {
  comments: CommentDto[];
  totalCount: number;
  pageId: number;
  take: number;
  pageCount: number;
}

export interface OrderItemDto {
  id: number;
  creationDate: string;
  orderId: number;
  inventoryId: number;
  count: number;
  price: number;
  totalPrice: number;
}

export interface OrderDto {
  id: number;
  creationDate: string;
  userId: number;
  status: number;
  address?: any;
  shippingMethod?: any;
  items: OrderItemDto[];
  totalPrice: number;
}

export interface SellerInventoryDto {
  id: number;
  creationDate: string;
  sellerId: number;
  productId: number;
  count: number;
  price: number;
  discountPercentage?: number;
}

export interface SellerDto {
  id: number;
  creationDate: string;
  userId: number;
  shopName: string;
  nationalCode: string;
  status: number;
  inventories: SellerInventoryDto[];
}

export interface SliderDto {
  id: number;
  creationDate: string;
  title: string;
  link: string;
  imageName: string;
}

export interface BannerDto {
  id: number;
  creationDate: string;
  link: string;
  imageName: string;
  position: number;
}

export interface CartItem {
  product: ProductDto;
  quantity: number;
  unitPrice: number;
}
