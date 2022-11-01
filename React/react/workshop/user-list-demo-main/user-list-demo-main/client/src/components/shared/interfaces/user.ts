export interface IUserBase {
  _id: string;
  imageUrl: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  createdAt: string;
  updatedAt: string;
}

export interface IUser extends IUserBase {
  address: {
    country: string;
    city: string;
    street: string;
    streetNumber: number;
  };
}

export interface ICreateOrUpdateUser {
  imageUrl: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address: {
    country: string;
    city: string;
    street: string;
    streetNumber: number;
  };
}
