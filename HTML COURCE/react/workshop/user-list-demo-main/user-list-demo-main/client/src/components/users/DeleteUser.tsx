import { FC, useEffect } from 'react';

import { faClose } from '@fortawesome/free-solid-svg-icons';

import Button from '../shared/Button';
import FormActions from '../shared/form/FormActions';
import LoadingSpinner from '../shared/LoadingSpinner';

import styles from './DeleteUser.module.css';

const DeleteUser: FC<{
  onClose: () => void;
  onConfirm: () => void;
  clearErrorMessage: () => void;
  isLoading: boolean;
  errorMessage: string;
}> = ({ onClose, onConfirm, clearErrorMessage, isLoading, errorMessage }) => {
  
  useEffect(() => {
    return () => { errorMessage &&  clearErrorMessage(); };
  }, [errorMessage, clearErrorMessage]);

  return (
    <div className={styles['confirm-container']}>
      <header className={styles['header']}>
        <h2>Are you sure you want to delete this account?</h2>
        <Button icon={faClose} action='close' onClick={onClose} />
      </header>
      <div className={styles.actions}>
        {isLoading && <LoadingSpinner className={styles.loader} />}
        <FormActions responseError={errorMessage} position={'center'}>
          <Button action={'save'} type='submit' disabled={isLoading} onClick={onConfirm}>
            Save
          </Button>
          <Button type='button' onClick={onClose} action={'cancel'}>
            Cancel
          </Button>
        </FormActions>
      </div>
    </div>
  );
};

export default DeleteUser;
