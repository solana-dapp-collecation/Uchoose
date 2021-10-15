import React from "react";
import './assets/styles/footer.scss';
import {FOOTER_LOGO} from "./constants/constants";
import team_logo from './assets/public/team-logo.png';

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
            <img src={team_logo} alt="LifeLoopTeam" style={{width: '120px', marginTop: '-15px'}}/>
        </span>
        </a>
    </footer>
  );
}
