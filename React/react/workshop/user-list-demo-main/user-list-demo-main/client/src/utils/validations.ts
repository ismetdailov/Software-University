export const validation = (message: string, regex: RegExp, value: string) =>
  regex.test(value.trim()) ? { message: '', isValid: true } : { message, isValid: false };

export const firstNameValidation = validation.bind(
  null,
  'First name should be at least 3 characters long!',
  /^[A-Za-z ]{3,}$/
);

export const lastNameValidation = validation.bind(
  null,
  'Last name should be at least 3 characters long!',
  /^[A-Za-z ]{3,}$/
);

export const emailValidation = validation.bind(
  null,
  'Email is not valid!',
  /^[A-Za-z0-9_.]+@[A-Za-z]+\.[A-Za-z]{2,3}$/
);

export const imageUrlValidation = validation.bind(null, 'ImageUrl is not valid!', /^https?:\/\/.+/);

export const phoneNumberValidation = validation.bind(
  null,
  'Phone number is not valid!',
  /^0[1-9]{1}[0-9]{8}$/
);

export const countryValidation = validation.bind(
  null,
  'Country should be at least 2 characters long!',
  /[a-zA-Z ]{2,}/
);

export const cityValidation = validation.bind(
  null,
  'City should be at least 3 characters long!',
  /[a-zA-Z ]{3,}/
);

export const streetValidation = validation.bind(
  null,
  'Street should be at least 3 characters long!',
  /[a-zA-Z ]{3,}/
);

export const streetNumberValidation = validation.bind(
  null,
  'Street number should be a positive number!',
  /^[1-9][0-9]*$/
);

export type IValidationFn = typeof firstNameValidation;
