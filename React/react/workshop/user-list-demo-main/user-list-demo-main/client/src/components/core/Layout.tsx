import { FC, ReactNode } from 'react';

import Footer from './Footer';
import Navigation from './Navigation';

import styles from './Layout.module.css';

const Layout: FC<{ children: ReactNode }> = ({ children }) => {
  return (
    <>
      <Navigation />
      <main className={styles.main}>{children}</main>
      <Footer />
    </>
  );
};

export default Layout;
