import { FC, useCallback, useEffect, useMemo, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { useSelector } from 'react-redux';

import { faUser } from '@fortawesome/free-solid-svg-icons';

import { useTypedDispatch } from '../../+store/store';
import { getUsersAction } from '../../+store/users/actions';
import * as selectors from '../../+store/users/selectors';

import Card from '../shared/Card';
import Pagination from '../shared/Pagination';
import SearchBar from '../shared/SearchBar';
import Table from './Table';
import Button from '../shared/Button';
import Modal from '../shared/Modal';
import AddOrUpdateUser from './AddOrUpdateUser';

import styles from './UsersList.module.css';

const UsersList: FC<{}> = () => {
  const dispatch = useTypedDispatch();
  const [searchParams] = useSearchParams();
  const [showAddNewUserModal, setShowAddNewUserModal] = useState(false);

  const page = Number(searchParams.get('page')) || 1;
  const limit = Number(searchParams.get('limit')) || 5;
  const sort = searchParams.get('sort');
  const order = searchParams.get('order');
  const search = searchParams.get('search');
  const criteria = searchParams.get('criteria');

  const users = useSelector(selectors.selectUserList);
  const count = useSelector(selectors.selectUserListCount);
  const isLoading = useSelector(selectors.selectUserListIsLoading);
  const errorMessage = useSelector(selectors.selectUserListErrorMessage);
  const maxPages = Math.ceil(count / limit) || 1;

  const headers = useMemo(
    () => ({
      imageUrl: 'Image',
      firstName: 'First name',
      lastName: 'Last name',
      email: 'Email',
      phoneNumber: 'Phone',
      createdAt: 'Created',
      action: 'Actions',
    }),
    []
  );

  const criterion: { value: string; text: string }[] = useMemo(
    () => [
      {
        value: 'firstName',
        text: 'First name',
      },
      {
        value: 'lastName',
        text: 'Last name',
      },
      {
        value: 'email',
        text: 'Email',
      },
      {
        value: 'phoneNumber',
        text: 'Phone',
      },
    ],
    []
  );

  const pageSizeOptions: number[] = useMemo(() => [5, 10, 15, 20], []);

  useEffect(() => {
    dispatch(
      getUsersAction(
        `users?page=${page}&limit=${limit}&sort=${sort}&order=${order}&search=${search}&criteria=${criteria}`
      )
    );
  }, [dispatch, page, limit, sort, order, criteria, search]);

  const onShowAddNewUserModalHandler = useCallback(
    () => setShowAddNewUserModal((prev) => !prev),
    []
  );

  return (
    <>
      {showAddNewUserModal && (
        <Modal onClose={onShowAddNewUserModalHandler}>
          <AddOrUpdateUser onClose={onShowAddNewUserModalHandler} mode='create' />
        </Modal>
      )}
      <Card classes={styles['users-container']}>
        <SearchBar title='Users' criterion={criterion} icon={faUser} />
        <Table
          title='users'
          headers={headers}
          content={users}
          isLoading={isLoading}
          errorMessage={errorMessage}
          defaultSort='createdAt'
          defaultOrder='asc'
        />
        <Button classes={styles['btn-add']} onClick={onShowAddNewUserModalHandler}>
          Add new user
        </Button>
        <Pagination
          maxPages={maxPages}
          pageSizeOptions={pageSizeOptions}
          showFirstLastButtons={true}
        />
      </Card>
    </>
  );
};

export default UsersList;
