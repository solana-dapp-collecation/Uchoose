import React from "react";
import "./assets/styles/Footer.module.scss";
import {FOOTER_LOGO} from "./constants/constants";

export function Footer() {

  return (
      <footer className="footer-component">
        <a
            href="#"
            target="_blank"
            rel="noopener noreferrer"
        >
            Made by{' '}
            <span className="logo">
            <img src={FOOTER_LOGO} alt="LifeLoopTeam" style={{width: '120px', marginTop: '-15px'}}/>
        </span>
        </a>
    </footer>
  );
}
