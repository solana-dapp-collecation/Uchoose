import React from "react";
import footerStyles from "../styles/Footer.module.css";
import {FOOTER_LOGO} from "../constants/constants";

export function Footer() {

  return (
      <footer className={footerStyles.footer}>
        <a
            href="#"
            target="_blank"
            rel="noopener noreferrer"
        >
            Made by{' '}
            <span className={footerStyles.logo}>
            <img src={FOOTER_LOGO} alt="LifeLoopTeam" style={{width: '120px', marginTop: '-15px'}}/>
        </span>
        </a>
    </footer>
  );
}
