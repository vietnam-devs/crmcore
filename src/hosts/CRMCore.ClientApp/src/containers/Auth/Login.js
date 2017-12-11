import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { Button } from 'reactstrap';
import { userManager } from 'configs';

class Login extends PureComponent {
  onLogout = e => {
    e.preventDefault();
    userManager.removeUser(); // removes the user data from sessionStorage
  };

  onLogin = e => {
    e.preventDefault();
    userManager.signinRedirect();
  };

  render() {
    const { profile } = this.props;
    return (
      <div>
        {!profile && (
          <div>
            <Button color="primary" onClick={this.onLogin}>
              Local Identity Provider
            </Button>
          </div>
        )}
        {profile && (
          <Button color="default" onClick={this.onLogout}>
            Logout
          </Button>
        )}
      </div>
    );
  }
}

function extractProfile(state) {
  if (state.oidc.user) {
    return state.oidc.user.profile;
  }
  return '';
}

function mapStateToProps(state, ownProps) {
  return {
    profile: extractProfile(state)
  };
}

export default connect(mapStateToProps)(Login);
