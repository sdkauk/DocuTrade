export enum TradeStatusName {
  Pending,
  Accepted,
  Declined,
}

export interface BasicUser {
  id: string;
  displayName: string;
}

export interface BasicBrief {
  id: string;
  name: string;
}

export interface BasicTrade {
  id: string;
  name: string;
}

export interface Document {
  id: string;
  extension: string;
}

export interface Image {
  id: string;
  extension: string;
}

export interface TradeStatus {
  id: string;
  name: TradeStatusName;
  description: string;
}

export interface User {
  id: string;
  objectIdentifier: string;
  firstName: string;
  lastName: string;
  email: string;
  displayName: string;
  briefs: BasicBrief[];
  trades: BasicTrade[];
}

export interface Brief {
  id: string;
  name: string;
  document: Document;
  preview: Image;
  author: BasicUser;
  owners: BasicUser[];
  description: string;
}

export interface Trade {
  id: string;
  name: string;
  buyer: BasicUser;
  seller: BasicUser;
  buyerBrief: BasicBrief;
  sellerBrief: BasicBrief;
  status: TradeStatus;
}

export interface UserBrief {
  id: string;
  userId: string;
  briefId: string;
}
