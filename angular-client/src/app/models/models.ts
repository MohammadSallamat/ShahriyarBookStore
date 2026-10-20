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
