import { FC, FormEvent, useEffect } from 'react';
import { useSelector } from 'react-redux';

import {
  faUser,
  faPhone,
  faMapMarkedAlt,
  faCity,
  faStreetView,
  faHome,
  faEnvelope,
  faImage,
  faClose,
} from '@fortawesome/free-solid-svg-icons';

import useInput from '../../hooks/use-input';
import * as validations from '../../utils/validations';
import { useTypedDispatch } from '../../+store/store';

import { ICreateOrUpdateUser } from '../shared/interfaces/user';
import { addNewUserAction, getUserAction, updateUserAction } from '../../+store/users/actions';
import { clearUser } from '../../+store/users/slice';
import * as selectors from '../../+store/users/selectors';

import Button from '../shared/Button';
import Form from '../shared/form/Form';
import FormActions from '../shared/form/FormActions';
import FormGroup from '../shared/form/FormGroup';
import FormRow from '../shared/form/FormRow';

import styles from './AddOrUpdateUser.module.css';

const AddOrUpdateUser: FC<{
  onClose: () => void;
  mode: 'create' | 'update';
  userId?: string;
}> = ({ onClose, userId, mode }) => {
  const dispatch = useTypedDispatch();
  const user = useSelector(selectors.selectCurrentUser);
  const errorMessage = useSelector(selectors.selectCurrentUserErrorMessage);
  const isLoading = useSelector(selectors.selectCurrentUserIsLoading);

  useEffect(() => {
    if (userId) { dispatch(getUserAction(userId)); }
    return () => { userId && dispatch(clearUser()); };
  }, [dispatch, userId]);

  const {
    value: firstNameValue,
    isValid: firstNameIsValid,
    errorMessage: firstNameErrorMessage,
    hasError: firstNameHasError,
    blurHandler: firstNameBlurHandler,
    changeHandler: firstNameChangeHandler,
    touched: firstNameIsTouched,
  } = useInput(validations.firstNameValidation, user?.firstName);

  const {
    value: lastNameValue,
    isValid: lastNameIsValid,
    errorMessage: lastNameErrorMessage,
    hasError: lastNameHasError,
    blurHandler: lastNameBlurHandler,
    changeHandler: lastNameChangeHandler,
    touched: lastNameIsTouched,
  } = useInput(validations.lastNameValidation, user?.lastName);

  const {
    value: emailValue,
    isValid: emailIsValid,
    errorMessage: emailErrorMessage,
    hasError: emailHasError,
    blurHandler: emailBlurHandler,
    changeHandler: emailChangeHandler,
    touched: emailIsTouched,
  } = useInput(validations.emailValidation, user?.email);

  const {
    value: phoneNumberValue,
    isValid: phoneNumberIsValid,
    errorMessage: phoneNumberErrorMessage,
    hasError: phoneNumberHasError,
    blurHandler: phoneNumberBlurHandler,
    changeHandler: phoneNumberChangeHandler,
    touched: phoneNumberIsTouched,
  } = useInput(validations.phoneNumberValidation, user?.phoneNumber);

  const {
    value: imageUrlValue,
    isValid: imageUrlIsValid,
    errorMessage: imageUrlErrorMessage,
    hasError: imageUrlHasError,
    blurHandler: imageUrlBlurHandler,
    changeHandler: imageUrlChangeHandler,
    touched: imageUrlIsTouched,
  } = useInput(validations.imageUrlValidation, user?.imageUrl);

  const {
    value: countryValue,
    isValid: countryIsValid,
    errorMessage: countryErrorMessage,
    hasError: countryHasError,
    blurHandler: countryBlurHandler,
    changeHandler: countryChangeHandler,
    touched: countryIsTouched,
  } = useInput(validations.countryValidation, user?.address.country);

  const {
    value: cityValue,
    isValid: cityIsValid,
    errorMessage: cityErrorMessage,
    hasError: cityHasError,
    blurHandler: cityBlurHandler,
    changeHandler: cityChangeHandler,
    touched: cityIsTouched,
  } = useInput(validations.cityValidation, user?.address.city);

  const {
    value: streetValue,
    isValid: streetIsValid,
    errorMessage: streetErrorMessage,
    hasError: streetHasError,
    blurHandler: streetBlurHandler,
    changeHandler: streetChangeHandler,
    touched: streetIsTouched,
  } = useInput(validations.streetValidation, user?.address.street);

  const {
    value: streetNumberValue,
    isValid: streetNumberIsValid,
    errorMessage: streetNumberErrorMessage,
    hasError: streetNumberHasError,
    blurHandler: streetNumberBlurHandler,
    changeHandler: streetNumberChangeHandler,
    touched: streetNumberIsTouched,
  } = useInput(validations.streetNumberValidation, user?.address.streetNumber?.toString());

  const formIsValid: boolean =
    firstNameIsValid &&
    lastNameIsValid &&
    emailIsValid &&
    imageUrlIsValid &&
    phoneNumberIsValid &&
    countryIsValid &&
    cityIsValid &&
    streetIsValid &&
    streetNumberIsValid;

  const formIsTouched: boolean =
    firstNameIsTouched ||
    lastNameIsTouched ||
    emailIsTouched ||
    imageUrlIsTouched ||
    phoneNumberIsTouched ||
    countryIsTouched ||
    cityIsTouched ||
    streetIsTouched ||
    streetNumberIsTouched;

  const onSubmitHandler = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!formIsValid) { return; }
    
    const userData: ICreateOrUpdateUser = {
      firstName: firstNameValue,
      lastName: lastNameValue,
      email: emailValue,
      imageUrl: imageUrlValue,
      phoneNumber: phoneNumberValue,
      address: {
        country: countryValue,
        city: cityValue,
        street: streetValue,
        streetNumber: Number(streetNumberValue),
      },
    };

    if (mode === 'create') {
      dispatch(addNewUserAction(userData, onClose));
    } else if (mode === 'update') {
      dispatch(updateUserAction(user!._id, userData, onClose));
    }
  };

  return (
    <div className={styles['user-container']}>
      <header className={styles['header']}>
        <h2> {mode === 'update' ? 'Edit User' : 'Add User'}</h2>
        <Button icon={faClose} action='close' onClick={onClose} />
      </header>
      <Form isLoading={isLoading} onSubmitHandler={onSubmitHandler}>
        <FormRow>
          <FormGroup
            name='firstName'
            errorMessage={firstNameErrorMessage}
            hasError={firstNameHasError}
            isValid={firstNameIsValid}
            icon={faUser}
            label='First name'
          >
            <input
              id='firstName'
              name='firstName'
              type='text'
              value={firstNameValue}
              onChange={firstNameChangeHandler}
              onBlur={firstNameBlurHandler}
            />
          </FormGroup>
          <FormGroup
            name='lastName'
            errorMessage={lastNameErrorMessage}
            hasError={lastNameHasError}
            isValid={lastNameIsValid}
            icon={faUser}
            label='Last name'
          >
            <input
              id='lastName'
              name='lastName'
              type='text'
              value={lastNameValue}
              onChange={lastNameChangeHandler}
              onBlur={lastNameBlurHandler}
            />
          </FormGroup>
        </FormRow>
        <FormRow>
          <FormGroup
            name='email'
            errorMessage={emailErrorMessage}
            hasError={emailHasError}
            isValid={emailIsValid}
            icon={faEnvelope}
            label='Email'
          >
            <input
              id='email'
              name='email'
              type='email'
              value={emailValue}
              onChange={emailChangeHandler}
              onBlur={emailBlurHandler}
            />
          </FormGroup>
          <FormGroup
            name='phoneNumber'
            errorMessage={phoneNumberErrorMessage}
            hasError={phoneNumberHasError}
            isValid={phoneNumberIsValid}
            icon={faPhone}
            label='Phone number'
          >
            <input
              id='phoneNumber'
              name='phoneNumber'
              type='text'
              value={phoneNumberValue}
              onChange={phoneNumberChangeHandler}
              onBlur={phoneNumberBlurHandler}
            />
          </FormGroup>
        </FormRow>
        <FormGroup
          name='imageUrl'
          errorMessage={imageUrlErrorMessage}
          hasError={imageUrlHasError}
          isValid={imageUrlIsValid}
          icon={faImage}
          label='Image Url'
        >
          <input
            id='imageUrl'
            name='imageUrl'
            type='text'
            value={imageUrlValue}
            onChange={imageUrlChangeHandler}
            onBlur={imageUrlBlurHandler}
          />
        </FormGroup>
        <FormRow>
          <FormGroup
            name='country'
            errorMessage={countryErrorMessage}
            hasError={countryHasError}
            isValid={countryIsValid}
            icon={faMapMarkedAlt}
            label='Country'
          >
            <input
              id='country'
              name='country'
              type='text'
              value={countryValue}
              onChange={countryChangeHandler}
              onBlur={countryBlurHandler}
            />
          </FormGroup>
          <FormGroup
            name='city'
            errorMessage={cityErrorMessage}
            hasError={cityHasError}
            isValid={cityIsValid}
            icon={faCity}
            label='City'
          >
            <input
              id='city'
              name='city'
              type='text'
              value={cityValue}
              onChange={cityChangeHandler}
              onBlur={cityBlurHandler}
            />
          </FormGroup>
        </FormRow>
        <FormRow>
          <FormGroup
            name='street'
            errorMessage={streetErrorMessage}
            hasError={streetHasError}
            isValid={streetIsValid}
            icon={faStreetView}
            label='Street'
          >
            <input
              id='street'
              name='street'
              type='text'
              value={streetValue}
              onChange={streetChangeHandler}
              onBlur={streetBlurHandler}
            />
          </FormGroup>
          <FormGroup
            name='streetNumber'
            errorMessage={streetNumberErrorMessage}
            hasError={streetNumberHasError}
            isValid={streetNumberIsValid}
            icon={faHome}
            label='Street number'
          >
            <input
              id='streetNumber'
              name='streetNumber'
              type='number'
              value={streetNumberValue}
              onChange={streetNumberChangeHandler}
              onBlur={streetNumberBlurHandler}
            />
          </FormGroup>
        </FormRow>
        <FormActions responseError={errorMessage || ''} position={'right'}>
          <Button
            disabled={!formIsValid || !formIsTouched ? true : false}
            type='submit'
            action={'save'}
          >
            Save
          </Button>
          <Button type='button' onClick={onClose} action={'cancel'}>
            Cancel
          </Button>
        </FormActions>
      </Form>
    </div>
  );
};

export default AddOrUpdateUser;
