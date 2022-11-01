import { FC, memo, useCallback, useState } from 'react';
import { useSelector } from 'react-redux';

import { faEdit, faTrash, faInfo } from '@fortawesome/free-solid-svg-icons';

import { useTransformDate } from '../../hooks/use-transform-date';
import { useTypedDispatch } from '../../+store/store';
import { deleteUserAction } from '../../+store/users/actions';
import { userErrorMessage } from '../../+store/users/slice';
import * as selectors from '../../+store/users/selectors';
import { IUserBase } from '../shared/interfaces/user';

import AddOrUpdateUser from './AddOrUpdateUser';
import Button from '../shared/Button';
import Modal from '../shared/Modal';
import DeleteUser from './DeleteUser';
import DetailUser from './DetailUser';

import styles from './TableItem.module.css';

interface ITableItem {
  headers: { [prop: string]: string };
  currentUser: IUserBase;
}

const TableItem: FC<ITableItem> = ({ headers, currentUser }) => {
  const dispatch = useTypedDispatch();
  const transformedDate = useTransformDate(currentUser.createdAt, { dateStyle: 'long' });
  const isLoading = useSelector(selectors.selectCurrentUserIsLoading);
  const errorMessage = useSelector(selectors.selectCurrentUserErrorMessage);

  const [showEditModal, setShowEditModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showDetailModal, setShowDetailModal] = useState(false);
  const onShowEditModalHandler = useCallback(() => setShowEditModal((prev) => !prev), []);
  const onShowDeleteModalHandler = useCallback(() => setShowDeleteModal((prev) => !prev), []);
  const onShowDetailModalHandler = useCallback(() => setShowDetailModal((prev) => !prev), []);

  const deleteUserHandler = useCallback((userId: string) => {
      dispatch(deleteUserAction(userId, onShowDeleteModalHandler));
    },
    [dispatch, onShowDeleteModalHandler]
  );

  const clearErrorMessageHandler = useCallback(() => {
    dispatch(userErrorMessage({ message: null }));
  }, [dispatch]);

  return (
    <>
      {showEditModal && (
        <Modal onClose={onShowEditModalHandler} key={currentUser._id + 'edit-modal'}>
          <AddOrUpdateUser
            onClose={onShowEditModalHandler}
            userId={currentUser._id}
            mode='update'
          />
        </Modal>
      )}

      {showDeleteModal && (
        <Modal onClose={onShowDeleteModalHandler} key={currentUser._id + 'delete-modal'}>
          <DeleteUser
            key={currentUser._id + 'delete-component'}
            onClose={onShowDeleteModalHandler}
            onConfirm={deleteUserHandler.bind(null, currentUser._id)}
            isLoading={isLoading}
            errorMessage={errorMessage || ''}
            clearErrorMessage={clearErrorMessageHandler}
          />
        </Modal>
      )}

      {showDetailModal && (
        <Modal onClose={onShowDetailModalHandler} key={currentUser._id + 'detail-modal'}>
          <DetailUser
            key={currentUser._id + 'detail-component'}
            userId={currentUser._id}
            isLoading={isLoading}
            errorMessage={errorMessage || ''}
            onClose={onShowDetailModalHandler}
          />
        </Modal>
      )}
      <tr key={currentUser._id}>
        {Object.keys(headers).map((key: string, i: number) =>
          currentUser.hasOwnProperty(key) ? (
            <td key={`${key}` + i}>
              {key === 'imageUrl' ? (
                <img
                  src={currentUser[key]}
                  alt={currentUser.firstName + '=avatar'}
                  className={styles.image}
                />
              ) : key === 'createdAt' ? (
                transformedDate
              ) : (
                currentUser[key as keyof typeof currentUser]
              )}
            </td>
          ) : null
        )}
        <td className={styles.actions}>
          <Button
            icon={faEdit}
            classes={`${styles['btn']} ${styles['edit-btn']}`}
            title='Edit'
            onClick={onShowEditModalHandler}
          />
          <Button
            icon={faTrash}
            classes={`${styles['btn']} ${styles['delete-btn']}`}
            title='Delete'
            onClick={onShowDeleteModalHandler}
          />
          <Button
            icon={faInfo}
            classes={`${styles['btn']} ${styles['info-btn']}`}
            title='Info'
            onClick={onShowDetailModalHandler}
          />
        </td>
      </tr>
    </>
  );
};

export default memo(TableItem);
