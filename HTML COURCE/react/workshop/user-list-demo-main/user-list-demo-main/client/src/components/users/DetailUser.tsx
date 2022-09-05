import { FC, useEffect } from 'react';
import { useSelector } from 'react-redux';

import { faClose } from '@fortawesome/free-solid-svg-icons';

import { useTransformDate } from '../../hooks/use-transform-date';
import { useTypedDispatch } from '../../+store/store';
import { getUserAction } from '../../+store/users/actions';
import { clearUser } from '../../+store/users/slice';
import * as selectors from '../../+store/users/selectors';

import Button from '../shared/Button';
import LoadingSpinner from '../shared/LoadingSpinner';

import styles from './DetailUser.module.css';

const DetailUser: FC<{
  userId: string;
  onClose: () => void;
  isLoading: boolean;
  errorMessage: string;
}> = ({ userId, onClose, isLoading, errorMessage }) => {
  const dispatch = useTypedDispatch();
  const currentUser = useSelector(selectors.selectCurrentUser);
  const createdDate = useTransformDate(currentUser?.createdAt || '', { dateStyle: 'full' });
  const updatedDate = useTransformDate(currentUser?.updatedAt || '', { dateStyle: 'full' });

  useEffect(() => {
    dispatch(getUserAction(userId));

    return () => { dispatch(clearUser()); };
  }, [dispatch, userId]);

  return (
    <div className={styles['detail-container']}>
      <header className={styles['header']}>
        <h2> User Detail</h2>
        <Button icon={faClose} action='close' onClick={onClose} />
      </header>
      <div className={styles.content}>
        {isLoading && <LoadingSpinner />}
        {!isLoading && !errorMessage && (
          <>
            <div className={styles['image-container']}>
              <img src={currentUser?.imageUrl} alt={currentUser?.firstName + '-profile'} />
            </div>
            <div className={styles['user-details']}>
              <p>
                User Id: <strong>{currentUser?._id}</strong>
              </p>
              <p>
                Full Name:{' '}
                <strong>
                  {currentUser?.firstName} {currentUser?.lastName}
                </strong>
              </p>
              <p>
                Email: <strong>{currentUser?.email}</strong>
              </p>
              <p>
                Phone Number: <strong>{currentUser?.phoneNumber}</strong>
              </p>
              <p>
                Address:{' '}
                <strong>
                  {currentUser?.address?.country}, {currentUser?.address?.city},{' '}
                  {currentUser?.address?.street} №{currentUser?.address?.streetNumber}
                </strong>
              </p>

              <p>
                Created on: <strong>{createdDate}</strong>
              </p>
              <p>
                Modified on: <strong>{updatedDate}</strong>
              </p>
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default DetailUser;
