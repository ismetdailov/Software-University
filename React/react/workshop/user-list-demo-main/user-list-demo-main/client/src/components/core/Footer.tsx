import { FC } from 'react';
import styles from './Footer.module.css';

const Footer: FC = () => {
    return (
        <footer className={styles.footer}>
            <p>Copyright © designed by Mihail Valkov</p>
        </footer>
    );
};

export default Footer;